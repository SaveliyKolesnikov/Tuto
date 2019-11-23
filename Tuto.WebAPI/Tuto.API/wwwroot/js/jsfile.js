var URL_PATH = "https://localhost:44367/";
var URL_SERVER = "https://localhost:44367/";

// login page js

function login_page(){
   var url = URL_SERVER + "oauth/authorize?returnUrl=" + URL_SERVER+"ChooseRole.html";
   document.querySelector("#login_url").href = url;
}






// for UserReg & TeacherReg js

function change_step_next(i){
    i--;
    if(i>0&&i<$('.steps').length){
        $('.steps')[i-1].className = "steps closed";
        $('.steps')[i].classList.remove('closed');
    }
}
function change_step_prev(i){
    if(i>0&&i<$('.steps').length){
        $('.steps')[i].className = "steps closed";
        $('.steps')[i-1].classList.remove('closed');
    }
    
}


// for TeacherReg js

function TeacherReg(){
 $.ajax({
    type: "GET", 
    async: false,
    url: URL_SERVER+"odata/Cities",
    success: function (data) {
        var obj = JSON.parse(data);
        var selectcity = document.getElementById('selectcity');
        obj['value'].forEach(function(element) {
            var name = element['Name'];
            var p = document.createElement("option");
            p.value = name;
            selectcity.appendChild(p);
        });

    }
});
 let daywork = $("#selectday").selectize({
    plugins: ['remove_button'],
    create: true,
    sortField: 'text'
});
 let citywork = $("#selectcity").selectize({
    plugins: ['remove_button'],
    create: true,
    maxItems: 2,
    sortField: 'Name',
    valueField: 'Name',
    labelField: 'Name',
    searchField: ['Name']
});
}


// for UserReg js

function UserReg(){
    $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
        url: URL_SERVER+"oauth/GetCurrentUserId",
        success: function (data) {
            var selectcity = document.getElementById('IdUser');
            selectcity.value = data;
        }
    });
    $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
        url: URL_SERVER+"odata/Cities",
        success: function (data) {
            console.log(data);
            var obj = data;
            var selectcity = document.getElementById('selectCity');
            obj['value'].forEach(function (element) {
                var name = element['Name'];
                var p = document.createElement("option");
                p.value = name;
                selectcity.appendChild(p);
            });

        }
    });
    let cityStudy = $("#selectCity").selectize({
        plugins: ['remove_button'],
        create: true,
        maxItems: 2,
        sortField: 'Name',
        valueField: 'Name',
        labelField: 'Name',
        searchField: ['Name']

    });
    
    
    $('#photo').change(function(){
        if(this.files&& this.files[0]){
            var reader = new FileReader();
            reader.onload = function() {
                var img = new Image();
                img.onload = function(){
                    var canvas = $("#canvas")[0];
                    var ctx = canvas.getContext('2d');
                    ctx.clearRect(0,0,canvas.width,canvas.height);
                    ctx.drawImage(img, 0, 0,img.width,img.height,0,0,200,200);
                }
                img.src = event.target.result;
            }
            reader.readAsDataURL(this.files[0]);
        }
    });
    

}
function sendForm_User(){
    var id = document.querySelector('#IdUser').value;
    var name = document.querySelector('#Name').value;
    var checkers = [
    $('#student-home').checked,
    $('#tutor-home').checked,
    $('#another-location').checked
    ];
    var selectCity = document.querySelector('#selectCity>option').value;
    var cityId = null;
    var bio = document.querySelector('#additional').value;
    var input_file = document.querySelector('#photo');
    var base64 = document.querySelector('#canvas').toDataURL('image/jpeg',0.7);
    var url = encodeURI(URL_SERVER+"Odata/Cities?$filter=Name eq '"+selectCity+"'");
    $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
        url: url,
        success: function (data) {
           cityId = data['value'][0]['Id'];
           console.log(cityId);
       }
   });



    var array_post;
    $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
        url: URL_SERVER+"Odata/Users("+id+")",
        success: function (data) {
           array_post = data;
           console.log(array_post);
       }
   });
    array_post["CityId"] = cityId;
    array_post["Picture"] = base64;
    array_post["Name"] = name;
    array_post["Description"] = bio;
    console.log(array_post);
    $.ajax({
        type: "PUT",
        headers: {'Content-type':'application/json'},
        dataType: 'json',
        async: false,
        data: JSON.stringify(array_post),
        url: URL_SERVER+"Odata/Users("+id+")",
        success: function (data, textStatus, xhr) {
            var status = xhr.status;
            switch(status){
                case 204:
                    location.href = URL_PATH+"profile-student.html";
                    break;
                default:
                    alert("ServerError");
                    break;
            }
            //document.location.href="profile-student.html";
        }
    });

}




// for Profile teacher js

function ProfileTeacher(){
    var AUTHENTICATE  = null;
    $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
        url: URL_SERVER+"oauth/GetCurrentUserId",
        success: function (data) {
            AUTHENTICATE = data;
        }
    });
    var modal = document.querySelector("#modal2"),
    modalOverlay = document.querySelector("#modal-overlay2"),
    closeButton = document.querySelector("#close-button2");
    let openButton = document.querySelector(".edit");


    closeButton.addEventListener("click", function () {
        toggle(modal);
        toggle(modalOverlay);
    });

    openButton.addEventListener("click", function () {
        toggle(modal);
        toggle(modalOverlay);
    });

    let comment = document.querySelector("#profile-comment");

    comment.addEventListener("click", function () {
        let first = document.querySelector(".first-slide");
        let second = document.querySelector(".second-slide");
        toggle(first);
        toggle(second);
    });

    let citywork = $("#selectcity").selectize({
        plugins: ['remove_button'],
        create: true,
        sortField: 'text'
    });

    let daywork = $("#selectday").selectize({
        plugins: ['remove_button'],
        create: true,
        sortField: 'text'
    });

    var USR_DATA = null;
    if(AUTHENTICATE!=null){
        $.ajax({
            type: "GET", 
            async: false,
            dataType: 'json',
            url: URL_SERVER+"Odata/Users("+AUTHENTICATE+")",
            success: function (data) {
               USR_DATA = data;
               console.log(USR_DATA);
           }
       });
        var photos = document.querySelectorAll(".User_Photo");
        [].forEach.call(photos, function(photo){
            photo.src = USR_DATA["Picture"];
        });
        var names = document.querySelectorAll(".User_Name");
        [].forEach.call(names, function(name){
            name.innerText = USR_DATA["Name"]+" "+USR_DATA["Surname"];
        });
    }
    else{
        document.location.href="log%20in.html";
    }
}
function toggle(el) {
    el.style.display = (el.style.display == 'none') ? 'block' : 'none'
}
// for Profile teacher js

function ProfileStudent(){
    var AUTHENTICATE  = null;
    $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
        url: URL_SERVER+"oauth/GetCurrentUserId",
        success: function (data) {
            AUTHENTICATE = data;
        }
    });
    var modal = document.querySelector("#modal2"),
    modalOverlay = document.querySelector("#modal-overlay2"),
    closeButton = document.querySelector("#close-button2");
    let openButton = document.querySelector(".edit");


    closeButton.addEventListener("click", function () {
        toggle(modal);
        toggle(modalOverlay);
    });

    openButton.addEventListener("click", function () {
        toggle(modal);
        toggle(modalOverlay);
    });


    let citywork = $("#selectcity").selectize({
        plugins: ['remove_button'],
        create: true,
        sortField: 'text'
    });

    let daywork = $("#selectday").selectize({
        plugins: ['remove_button'],
        create: true,
        sortField: 'text'
    });

    var USR_DATA = null;
    if(AUTHENTICATE!=null){
        $.ajax({
            type: "GET", 
            async: false,
            dataType: 'json',
            url: URL_SERVER+"Odata/Users("+AUTHENTICATE+")",
            success: function (data) {
               USR_DATA = data;
               console.log(USR_DATA);
           }
       });
        var photos = document.querySelectorAll(".User_Photo");
        [].forEach.call(photos, function(photo){
            photo.src = USR_DATA["Picture"];
        });
        var names = document.querySelectorAll(".User_Name");
        [].forEach.call(names, function(name){
            name.innerText = USR_DATA["Name"]+" "+USR_DATA["Surname"];
        });
    }
    else{
        document.location.href="log%20in.html";
    }
}