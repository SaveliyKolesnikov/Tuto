var URL_PATH = "file:///C:/Users/nikto/Desktop/APVZ/APVZ_LOCAL/";
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
                url: URL_SERVER+"GetCurrentUserId",
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
                  console.log("start");
                  var obj = JSON.parse(data);
                  var selectcity = document.getElementById('selectCity');
                  obj['value'].forEach(function(element) {
                    var name = element['Name'];
                    var p = document.createElement("option");
                    p.value = name;
                    console.log(name+"1");
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


}
function sendForm_User(){
        var id = $('#IdUser').value;
        var name = $('#Name').value;
        var checkers = [
        $('#student-home').checked,
        $('#tutor-home').checked,
        $('#another-location').checked
        ];
        var selectCity = $('#selectCity').value;
        var bio = $('#additional').value;

        document.location.href="profile-student.html";
 $.ajax({
        type: "GET", 
        async: false,
        dataType: 'json',
                url: URL_SERVER+"Odata/User",
                success: function (data) {
                    var selectcity = document.getElementById('IdUser');
                    selectcity.value = data;
                }
            });
    }
