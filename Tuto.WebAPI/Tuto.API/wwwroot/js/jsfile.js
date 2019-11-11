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
            reader.onloadend = function() {
                document.querySelector('#base64').value = reader.result;
                document.querySelector('#img_uploaded').src = reader.result;
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
    var base64 = document.querySelector('#base64').value;
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

    var array_post = {
        "Name": name,
        "Description": bio,
        "CityId": cityId,
        "Picture": base64
    };
    console.log(array_post);
    $.ajax({
        type: "POST", 
        async: false,
        data: array_post,
        url: URL_SERVER+"Odata/Users("+id+")",
        success: function (data) {
            console.log(data);
            //document.location.href="profile-student.html";
        }
    });
   
}
