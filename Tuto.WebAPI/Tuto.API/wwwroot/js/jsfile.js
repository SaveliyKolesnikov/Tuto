var URL_PATH = "https://localhost:44367/";
var URL_SERVER = "https://localhost:44367/";
var LOGIN_PAGE = "log%20in.html";
var CHOOSE_ROLE = "ChooseRole.html";
var PROFILE_PAGE_STUDENT = "profile-student.html";
var PROFILE_PAGE_TEACHER = "profile-teacher.html";
var TEACHER = "teacher.html?id=";
var SEARCH_TUTOR = "searchTutor.html";

// login page js
function login_page(){
	var url = URL_SERVER + "oauth/authorize?returnUrl=" + URL_SERVER+"ChooseRole.html";
	document.querySelector("#login_url").href = url;
}
function playloaderOn(){
	var play = s("#hellopreloader_preload");
	play.style.opacity = "1";
	play.style.display = "block";
}
function playloaderOff(){
	var play = s("#hellopreloader_preload");
	play.style.opacity = "0.5";
	play.style.display = "none";
}
function Render(){
	var play = document.createElement("div");
	var round = document.createElement("div");
	play.setAttribute("id", "hellopreloader");
	round.setAttribute("id", "hellopreloader_preload");
	play.appendChild(round);
	document.body.appendChild(play);
	var hellopreloader = document.getElementById("hellopreloader_preload");
	function fadeOutnojquery(el){
		el.style.opacity = 1;
		var interhellopreloader = setInterval(function(){
			el.style.opacity = el.style.opacity - 0.05;
			if (el.style.opacity <=0.05){
				clearInterval(interhellopreloader);
				hellopreloader.style.display = "none";
			}
		},16);
	}
	window.onload = function(){
		setTimeout(function(){fadeOutnojquery(hellopreloader);},1000);
	};
}
function renderRight() {
	let tutor_Search = s("#tutorSearch");
	tutor_Search.addEventListener("click", function () {
		location.href = URL_PATH+SEARCH_TUTOR;
	});
	let open_messages = s("#open_messages");
	open_messages.addEventListener("click", function () {
		let first = s(".start-right");
		let second = s(".profile-chat");
		toggle(first);
		toggle(second);
	});
	let back_from_messages = s("#back_from_messages");
	back_from_messages.addEventListener("click", function () {
		let first = s(".start-right");
		let second = s(".profile-chat");
		toggle(first);
		toggle(second);
	});	
	let back_to_messages = s("#back_to_messages");
	back_to_messages.addEventListener("click", function () {
		let first = s(".profile-chat");
		let second = s(".profile-chat-message");
		toggle(first);
		toggle(second);
	});	
}
function renderLeft(){
	let my_profile_link = s("#my_profile_link");
	my_profile_link.href = URL_PATH+CHOOSE_ROLE;
}
//Check Login status
function isNew(){
	if(getUser(Login())["IsProfileFilled"]){
		if(getUser(Login())['TeacherInfo'])
			location.href = URL_PATH+PROFILE_PAGE_TEACHER;
		else
			location.href = URL_PATH+PROFILE_PAGE_STUDENT;
	}
	else
		return true;
	return false;
}
function isProfileFilled(){
	if(!getUser(Login())["IsProfileFilled"])
		location.href = URL_PATH+CHOOSE_ROLE;
}
function isTeacher(user){
	if(user['TeacherInfo'])
		return true;
	return false;
}
function printRole(block, user){
	var block = sa(block);
	block.forEach(function (div) {
		div.innerText = (user['TeacherInfo'])?"Teacher":"Student";
	});
}
function Login(){
	var AUTHENTICATE = null;
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"oauth/GetCurrentUserId",
		success: function (data) {
			AUTHENTICATE = data;
		}
	});
	if(AUTHENTICATE!=null&&AUTHENTICATE!="")
		return AUTHENTICATE;
	location.href = URL_PATH+LOGIN_PAGE;
}
function Logout(){
	$.ajax({
		type: "POST",
		dataType: 'json',
		async: false,
		data: null,
		url: URL_SERVER+"oauth/LogOut",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 200:
				location.href = URL_PATH+LOGIN_PAGE;
				break;
				default:
				alert("ServerError");
				break;
			}
		}
	});
}
function DeleteTeacher(){
	playloaderOn();
	var id = getUser(Login())['TeacherInfo']["Id"];
	deleteTeachersSubject(getUser(Login())['TeacherInfo']["TeacherSubjects"]);
	$.ajax({
		type: "DELETE", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"Odata/TeacherInfos("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				DeleteUser();
				break;
				default:
				alert("ServerError");
				break;
			}
		}
	});
}
function getUser(id){
	var USR = null;
	//https://localhost:44367/Odata/Users?$expand=TeacherInfo&$filter=Id%20eq%20a4460ac1-4414-ea11-b815-00155d0ace00
	var url = URL_SERVER+"Odata/Users?$expand=TeacherInfo($expand=TeacherSubjects)&$filter=Id%20eq%20"+id;
	console.log(url);
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data['value'][0]);
			USR = data['value'][0];
		}
	});
	if(USR)
		USR["Address"] = JSON.parse(USR["Address"]);
	return USR;
}
function DeleteUser(){
	var id = Login();
	$.ajax({
		type: "POST",
		dataType: 'json',
		async: false,
		data: null,
		url: URL_SERVER+"oauth/LogOut",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 200:
				break;
				default:
				return;
				break;
			}
		}
	});
	$.ajax({
		type: "DELETE", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"Odata/Users("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				location.href = URL_PATH+LOGIN_PAGE;
				break;
				default:
				alert("ServerError");
				break;
			}
		}
	});
}
function getAllMessages(userId){
	var arr = [];
	//https://localhost:44367//OData/ChatMessages?$filter=RecipientId%20eq%20869aad0c-5017-ea11-b815-00155d0ace00&$orderby=SendTime%20desc
	var url = URL_SERVER+"OData/ChatMessages?$filter=RecipientId%20eq%20"+userId+"%20or%20SenderId%20eq%20"+userId+"&$orderby=SendTime%20desc";
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			data['value'].forEach(function (message) {
				arr.push(message);
			});
		}
	});
	return arr;
}
function getUserChats(arr_all_messages,blockId,userId){
	var block = s(blockId);
	$(blockId).empty();
	var chats = arr_all_messages.sort(function(a,b){return a["SenderId"] < b["SenderId"] ? -1 : 1;}).reduce(function(arr, el){
		if(!arr.length || arr[arr.length - 1]["SenderId"] != el["SenderId"]) {
			arr.push(el);
		}
		return arr;
	}, []);
	console.log(chats);
	chats.forEach(function (chat) {
		var sender = getUser(chat["SenderId"]);
		var review = document.createElement("div");
		review.classList.add('review');
		review.setAttribute("id",sender["Id"]);
		review.setAttribute("onclick",'getSpecificChat("'+sender['Id']+'");');
		block.appendChild(review);
		//=============================
		var img_profile_mini = document.createElement("div");
		img_profile_mini.classList.add('img_profile_mini');
		review.appendChild(img_profile_mini);
		//=============================
		var img = document.createElement("img");
		img.setAttribute("src", sender["Picture"]);
		img_profile_mini.appendChild(img);
		//=============================
		var sender_info = document.createElement("div");
		sender_info.classList.add('sender_info');
		review.appendChild(sender_info);
		//=============================
		var p_fio = document.createElement("p");
		p_fio.classList.add('text-small');
		p_fio.innerText = sender["Name"] + " " + sender["Surname"];
		sender_info.appendChild(p_fio);
		//=============================
		var p_time = document.createElement("p");
		p_time.classList.add('text-nano');
		p_time.classList.add('message-time');
		p_time.innerText = new Date(chat["SendTime"]).toLocaleTimeString();
		sender_info.appendChild(p_time);
		//=============================
		var p_message = document.createElement("p");
		p_message.classList.add('text-nano');
		p_message.classList.add('message-text');
		p_message.innerText = chat["Text"];
		sender_info.appendChild(p_message);
		//=============================
	});



	// <div class="review">
	
	//     <div class="img_profile_mini">
	//         <img src="img/Profile.JPG" alt="" class="">
	//     </div>
	//     <div class="sender_info">
	//         <p class="text-small">Katie Atakulova</p>
	//         <p class="text-nano message-time">11:15
	//         </p>
	//         <p class="text-nano message-text">Govno! Vse ploho. Nastya top.Govno! Vse ploho. Nastya
	//             top.Govno! Vse ploho. Nastya top.Govno! Vse ploho. Nastya top.Govno! Vse
	//         </p>
	
	//     </div>
	
	// </div>
}
function getSpecificChat(idUser){
	let first = s(".profile-chat");
	let second = s(".profile-chat-message");
	toggle(first);
	toggle(second);
}
function getAllTeachers(){
	var arr = [];
	//https://localhost:44367/Odata/Users?$expand=TeacherInfo($expand=TeacherSubjects)&$filter=(TeacherInfo+ne+null)
	var url = URL_SERVER+"Odata/Users?$expand=TeacherInfo($expand=TeacherSubjects)&$filter=(TeacherInfo+ne+null)";
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			data['value'].forEach(function (user) {
				user["Address"] = JSON.parse(user["Address"]);
				arr.push(user);
			});
		}
	});
	return arr;
}
function renderAllTeachers(teachers,block){
	teachers.forEach(function (teacher) {

		var tutor_result = document.createElement("div");
		tutor_result.classList.add('tutor-result');
		tutor_result.setAttribute("id", teacher["Id"]);
		block.appendChild(tutor_result);
		// ====================
		var profile_head = document.createElement("div");
		profile_head.classList.add('profile-head');
		tutor_result.appendChild(profile_head);
		// ====================
		var img_profile_maxi = document.createElement("div");
		img_profile_maxi.classList.add('img_profile_maxi');
		profile_head.appendChild(img_profile_maxi);
		// ====================
		var img = document.createElement("img");
		img.setAttribute("src", teacher["Picture"]);
		img_profile_maxi.appendChild(img);
		// ====================
		var info_profile_maxi = document.createElement("div");
		info_profile_maxi.classList.add('info_profile_maxi');
		profile_head.appendChild(info_profile_maxi);
		// ====================
		var p_subjects = document.createElement("p");
		p_subjects.classList.add('text-nano');
		var arr = [];
		var userSubjects = teacher["TeacherInfo"]["TeacherSubjects"];
		userSubjects.forEach(function (element) {
			arr.push(getSubjectName(element['SubjectId']));
		});
		p_subjects.innerText = arr.join(", ");
		info_profile_maxi.appendChild(p_subjects);
		// ====================
		var p_fio = document.createElement("p");
		p_fio.classList.add('text-medium');
		p_fio.innerText = teacher["Name"] + " " + teacher["Surname"];
		info_profile_maxi.appendChild(p_fio);
		// ====================
		var div_city = document.createElement("div");
		div_city.innerHTML = "<span class='icon-city'></span>"
		info_profile_maxi.appendChild(div_city);
		// ====================
		var span_city = document.createElement("span");
		span_city.classList.add("text-small");
		span_city.innerHTML = getCityName(teacher["CityId"]);
		div_city.appendChild(span_city);
		// ====================
		var p_about = document.createElement("p");
		p_about.classList.add('text-nano');
		p_about.classList.add('add-text');
		p_about.innerText = teacher["Description"];
		info_profile_maxi.appendChild(p_about);
		// ====================
		var tutor_right = document.createElement("div");
		tutor_right.classList.add('tutor-right');
		tutor_right.innerHTML = '<div><div class="page__group"><div class="rating"><input type="radio" name="rating-star" class="rating__control" id="rc1"><input type="radio" name="rating-star" class="rating__control" id="rc2"><input type="radio" name="rating-star" class="rating__control" id="rc3"><input type="radio" name="rating-star" class="rating__control" id="rc4"><input type="radio" name="rating-star" class="rating__control" id="rc5"><label for="rc1" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">1</span></label><label for="rc2" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">2</span></label><label for="rc3" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">3</span></label><label for="rc4" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">4</span></label><label for="rc5" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">5</span></label></div></div><svg style="display: none" width="20" height="19" viewBox="0 0 20 19" fill="none"xmlns="http://www.w3.org/2000/svg"><path id="star" d="M10 0.809018L12.0074 6.98708L12.0635 7.15983H12.2451H18.7411L13.4858 10.9781L13.3388 11.0848L13.3949 11.2576L15.4023 17.4357L10.1469 13.6174L10 13.5106L9.85305 13.6174L4.59768 17.4357L6.60505 11.2576L6.66118 11.0848L6.51423 10.9781L1.25886 7.15983H7.75486H7.9365L7.99262 6.98708L10 0.809018Z" </div>';
		tutor_result.appendChild(tutor_right);
		// ====================
		var p_price = document.createElement("p");
		p_price.classList.add('text-medium');
		p_price.innerText = "$"+teacher["TeacherInfo"]["MinimumWage"]+"/hour";
		tutor_right.appendChild(p_price);
		// ====================
		var button_view = document.createElement("button");
		button_view.classList.add('button');
		button_view.classList.add('accent_button');
		button_view.classList.add('button_upper');
		button_view.setAttribute("onclick","location.href='"+URL_PATH+TEACHER+teacher["Id"]+"';");
		button_view.innerText = "View profile";
		tutor_right.appendChild(button_view);


		//   <div class="tutor-result">
        //     <div class="profile-head">
        //         <div class="img_profile_maxi">
        //             <img src="img/Profile.JPG" alt="" class="" />
        //         </div>
        //         <div class="">
        //             <p class="text-nano">English,Deutsch</p>
        //             <p class="text-medium">Katie Atakulova</p>
        //             <div>
        //                 <span class="icon-city"></span>
        //                 <span class="text-small">Kharkiv</span>
        //             </div>
        //             <p class="text-nano add-text">In the supplemented information, you can indicate the number of
        //                 students
        //                 with whom you want to study, features of the approach, as well as any information that you
        //             would like to convey to the student</p>
        //         </div>
        //     </div>
        //     <div class="tutor-right">
        //         <div><div class="page__group"><div class="rating"><input type="radio" name="rating-star" class="rating__control" id="rc1" checked><input type="radio" name="rating-star" class="rating__control" id="rc2"><input type="radio" name="rating-star" class="rating__control" id="rc3"><input type="radio" name="rating-star" class="rating__control" id="rc4"><input type="radio" name="rating-star" class="rating__control" id="rc5"><label for="rc1" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">1</span></label><label for="rc2" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">2</span></label><label for="rc3" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">3</span></label><label for="rc4" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">4</span></label><label for="rc5" class="rating__item"><svg class="rating__star"><use xlink:href="#star"></use></svg><span class="rating__label">5</span></label></div></div><svg style="display: none" width="20" height="19" viewBox="0 0 20 19" fill="none"xmlns="http://www.w3.org/2000/svg"><path id="star" d="M10 0.809018L12.0074 6.98708L12.0635 7.15983H12.2451H18.7411L13.4858 10.9781L13.3388 11.0848L13.3949 11.2576L15.4023 17.4357L10.1469 13.6174L10 13.5106L9.85305 13.6174L4.59768 17.4357L6.60505 11.2576L6.66118 11.0848L6.51423 10.9781L1.25886 7.15983H7.75486H7.9365L7.99262 6.98708L10 0.809018Z" /></s</div>
        //         <p class="text-medium">$30/hour</p>
        //         <button class="button accent_button button_upper">
        //             Send message
        //         </button>
        //     </div>
        // </div>






    });
}
function getCityId(name){
	var city = null;
	var url = encodeURI(URL_SERVER+"Odata/Cities?$filter=Name eq '"+name+"'");
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data);
			city = data['value'][0]['Id'];
			
		}
	});
	return city;
}
function getCityName(id){
	var city = null;
	var url = encodeURI(URL_SERVER+"Odata/Cities("+id+")");
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data);
			city = data['Name'];
			
		}
	});
	return city;
}
function setCities(id){
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"odata/Cities",
		success: function (data) {
			console.log(data);
			var obj = data;
			var selectcity = s(id);
			obj['value'].forEach(function (element) {
				var name = element['Name'];
				var id = element['Id']
				var p = document.createElement("option");
				p.value = name;
				p.setAttribute("id",id);
				selectcity.appendChild(p);
			});
		}
	});
	let cityStudy = $(id).selectize({
		plugins: ['remove_button'],
		create: true,
		maxItems: 2,
		sortField: 'Name',
		valueField: 'Name',
		labelField: 'Name',
		searchField: ['Name']

	});
}
function getSubjectId(name){
	var subject = null;
	var url = encodeURI(URL_SERVER+"Odata/Subjects?$filter=Name eq '"+name+"'");
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data);
			subject = data['value'][0]['Id'];
			
		}
	});
	return subject;
}
function getSubjectName(id){
	var subject = null;
	var url = encodeURI(URL_SERVER+"Odata/Subjects("+id+")");
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: url,
		success: function (data) {
			console.log(data);
			subject = data['Name'];
			
		}
	});
	return subject;
}
function setSubjectsForm(id){
	$.ajax({
		type: "GET", 
		async: false,
		dataType: 'json',
		url: URL_SERVER+"odata/Subjects",
		success: function (data) {
			console.log(data);
			var obj = data;
			var selectsubject = s(id);
			obj['value'].forEach(function (element) {
				var name = element['Name'];
				var id = element['Id']
				var p = document.createElement("option");
				p.value = name;
				p.setAttribute("id",id);
				selectsubject.appendChild(p);
			});
		}
	});
	let subjectStudy = $(id).selectize({
		plugins: ['remove_button'],
		create: true,
		maxItems: 2,
		sortField: 'Name',
		valueField: 'Name',
		labelField: 'Name',
		searchField: ['Name']

	});
}
function setSubjectsProfile(id,user){
	var selectsubject = s(id);
	var arr = [];
	var userSubjects = user["TeacherInfo"]["TeacherSubjects"];
	userSubjects.forEach(function (element) {
		arr.push(getSubjectName(element['SubjectId']));
	});
	selectsubject.innerText = arr.join(", ");	
}
function setTeachersSubject(subjects,teacherId){
	var flag = false;
	subjects.forEach(function (elementId) {
		var arr = {
			"TeacherInfoId": teacherId,
			"SubjectId": elementId
		}
		$.ajax({
			type: "POST",
			headers: {'Content-type':'application/json'},
			dataType: 'json',
			async: false,
			data: JSON.stringify(arr),
			url: URL_SERVER+"/odata/TeacherSubjects",
			success: function (data, textStatus, xhr) {
				var status = xhr.status;
				switch(status){
					case 201:
					flag = true;
					break;
					default:
					console.log("ServerError");
					break;
				}

			}
		});
	});	
	if(flag)
		location.href = URL_PATH+PROFILE_PAGE_TEACHER;
}
function deleteTeachersSubject(TeacherSubjects){
	TeacherSubjects.forEach(function (element) {
		var subjectId = element["Id"];
		$.ajax({
			type: "DELETE", 
			async: false,
			dataType: 'json',
			url: URL_SERVER+"Odata/TeacherSubjects("+subjectId+")",
			success: function (data, textStatus, xhr) {
				var status = xhr.status;
				switch(status){
					case 204:
					console.log("deleting - ok");
					break;
					default:
					console.log("deleting - error");
					break;
				}
			}
		});
	});
}
function setTeacherInfo(user,MinimumWage,PreferredDaysOfWeek,subjectsIds){
	var array_post = {
		"UserId": user["Id"],
		"MinimumWage": MinimumWage,
		"PreferredDaysOfWeek": PreferredDaysOfWeek
	}
	console.log(array_post);
	$.ajax({
		type: "POST",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(array_post),
		url: URL_SERVER+"/odata/TeacherInfos",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 201:
				setTeachersSubject(subjectsIds,getUser(user["Id"])["TeacherInfo"]["Id"]);
				// location.href = URL_PATH+PROFILE_PAGE_TEACHER;
				break;
				default:
				alert("ServerError");
				break;
			}

		}
	});
}
function updateTeacherInfo(user,MinimumWage,PreferredDaysOfWeek,subjectsIds){
	var array_post = {
		"UserId": user["Id"],
		"MinimumWage": MinimumWage,
		"PreferredDaysOfWeek": PreferredDaysOfWeek
	}
	$.ajax({
		type: "PUT",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(array_post),
		url: URL_SERVER+"Odata/TeacherInfos("+user["TeacherInfo"]["Id"]+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				deleteTeachersSubject(user["TeacherInfo"]["TeacherSubjects"]);
				setTeachersSubject(subjectsIds,user["TeacherInfo"]["Id"]);
				break;
				default:
				alert("ServerError");
				break;
			}

		}
	});
}
function uploadPhoto(upload,canvas){
	$("#photo").change(function(){
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
function parseURLParams(url){
	var queryStart = url.indexOf("?") + 1,
	queryEnd   = url.indexOf("#") + 1 || url.length + 1,
	query = url.slice(queryStart, queryEnd - 1),
	pairs = query.replace(/\+/g, " ").split("&"),
	parms = {}, i, n, v, nv;

	if (query === url || query === "") return;

	for (i = 0; i < pairs.length; i++) {
		nv = pairs[i].split("=", 2);
		n = decodeURIComponent(nv[0]);
		v = decodeURIComponent(nv[1]);

		if (!parms.hasOwnProperty(n)) parms[n] = [];
		parms[n].push(nv.length === 2 ? v : null);
	}
	return parms;
}
// for select objects
function s(obj){
	return document.querySelector(obj);
}
function sa(obj){
	return document.querySelectorAll(obj);
}
//for ChooseRole js
function ChooseRole(AUTHENTICATE){
	var closeButton = document.querySelector("#close-button");
	closeButton.addEventListener("click", function () {
		let form = document.forms.choose_role;
		let elem = form.elements.choose_role_radio;
		var radioActive = false;
		for (prop in elem) {
			if (elem[prop].checked) {
				radioActive = true;
			}
		}

		if (radioActive) {
			if (elem[0].checked) {
				document.location.href=URL_PATH+"UserReg.html";
			} else if (elem[1].checked) {
				document.location.href=URL_PATH+"TeacherReg.html";
			}
		} else {
			alert("You didn`t choose the role")
		}
	});
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
function toggle(el) {
	el.style.display = (el.style.display == 'none') ? 'block' : 'none'
}
// for TeacherReg js
function TeacherReg(AUTHENTICATE){
	var idUser = s('#IdUser');
	idUser.value = AUTHENTICATE;
	setCities('#selectCity'); 
	setSubjectsForm('#selectSubject');
	let daywork = $("#selectDay").selectize({
		plugins: ['remove_button'],
		create: true,
		sortField: 'text'
	});
	uploadPhoto("#photo","#canvas");
}
function sendForm_Teacher(id){
	playloaderOn();
	var userId = id;
	var MinimumWage = s("#minPrice").value;
	var daysArr = sa("#selectDay>option");
	var PreferredDaysOfWeek = [];
	daysArr.forEach(function (element) {
		PreferredDaysOfWeek.push(element.value);
	});

	var selectSubject = sa('#selectSubject>option');
	var subjectsIds = [];
	selectSubject.forEach(function (element) {
		var name = element.value;
		subjectsIds.push(getSubjectId(element.value));
	});
	

	PreferredDaysOfWeek = PreferredDaysOfWeek.join(',');
	console.log(PreferredDaysOfWeek);
	setTeacherInfo(getUser(id),MinimumWage,PreferredDaysOfWeek,subjectsIds);
	playloaderOff();
}
// for UserReg js
function UserReg(AUTHENTICATE){

	var idUser = document.getElementById('IdUser');
	idUser.value = AUTHENTICATE;
	setCities('#selectCity');   
	uploadPhoto("#photo","#canvas");
}
function sendForm_User(a){
	playloaderOn();
	var id = s('#IdUser').value;
	var name = s('#Name').value;
	var surname = s('#Surname').value;
	var checkers = {
		'Student`s home':s('#student-home').checked,
		'Tutors`s home':s('#tutor-home').checked,
		'Another location':s('#another-location').checked
	};
	var selectCity = s('#selectCity>option');
	var bio = s('#additional').value;
	var input_file = s('#photo');
	var base64 = s('#canvas').toDataURL('image/jpeg',0.7);
	if(!id||!name||!surname||!selectCity||!bio||!input_file||!base64){
		alert("Something Wrong");
		return;
	}
	var cityId = getCityId(selectCity.value);
	var array_post = getUser(id);
	array_post["IsProfileFilled"] = true;
	array_post["CityId"] = cityId;
	array_post["Picture"] = base64;
	array_post["Name"] = name;
	array_post["Surname"] = surname;
	array_post["Address"] = JSON.stringify(checkers);
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
				if(!a){
					location.href = URL_PATH+PROFILE_PAGE_STUDENT;
				}
				break;
				default:
				alert("ServerError");
				break;
			}

		}
	});
	if(a){
		sendForm_Teacher(id);
	}
	playloaderOff();
}
// for Profile teacher js
function ProfileTeacher(AUTHENTICATE){
	getUserChats(getAllMessages(AUTHENTICATE),".message");
	isProfileFilled();
	renderRight();
	renderLeft();
	setCities('#selectCity');
	setSubjectsForm('#selectSubject');
	setSubjectsProfile("#subjectsProfile",getUser(Login()));
	uploadPhoto("#photo","#canvas");
	var modal = s("#modal2"),
	modalOverlay = s("#modal-overlay2"),
	closeButton = s("#close-button2");
	let openButton = s(".edit");
	closeButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});
	openButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});
	let comment = s("#profile-comment");
	comment.addEventListener("click", function () {
		let first = s(".first-slide");
		let second = s(".second-slide");
		toggle(first);
		toggle(second);
	});
	let daywork = $("#selectday").selectize({
		plugins: ['remove_button'],
		create: true
	});
	var block = s(".chat");
	block.scrollTop = block.scrollHeight;

	var USR_DATA = getUser(AUTHENTICATE);
	s("#place").innerText = getCityName(USR_DATA["CityId"]);
	s("#additional_text").innerText = USR_DATA["Description"] 
	s("#price").innerText = USR_DATA["TeacherInfo"]["MinimumWage"];
	var location = s("#location");
	location.innerHTML = '';
	for(var prop in USR_DATA["Address"]){
		if(USR_DATA["Address"][prop]){
			var span = document.createElement('span');
			span.className = 'location text-nano';
			span.innerText = prop;
			location.appendChild(span);
		}
	}
	var dates = USR_DATA["TeacherInfo"]["PreferredDaysOfWeek"].split(", ");
	dates.forEach(function (element) {
		$("[name='"+element+"']").addClass('active').removeClass('passive');
	});
	var photos = sa(".CurrentUser_Photo");
	[].forEach.call(photos, function(photo){
		photo.src = USR_DATA["Picture"];
	});
	var names = sa(".CurrentUser_Name");
	[].forEach.call(names, function(name){
		name.innerText = USR_DATA["Name"]+" "+USR_DATA["Surname"];
	}); 
	var canvas = s("#canvas");
	var ctx = canvas.getContext("2d");

	var image = new Image();
	image.onload = function() {
		ctx.drawImage(image, 0, 0);
	};
	image.src =USR_DATA["Picture"];
	s("#change_name").value = USR_DATA["Name"];
	s("#change_surname").value = USR_DATA["Surname"];
	s("#change_additional").value = USR_DATA["Description"];
	s("#change_minimalprice").value = USR_DATA["TeacherInfo"]["MinimumWage"];
	s("#student-home").checked = USR_DATA["Address"]['Student`s home'];
	s("#tutor-home").checked = USR_DATA["Address"]['Tutors`s home'];
	s("#another-location").checked = USR_DATA["Address"]['Another location'];
}
function ChangeProfileTeacher(AUTHENTICATE){
	playloaderOn();
	var USR_DATA = getUser(AUTHENTICATE);
	var id = AUTHENTICATE;
	var name = s('#change_name').value;
	var surname = s('#change_surname').value;
	var checkers = {
		'Student`s home':s('#student-home').checked,
		'Tutors`s home':s('#tutor-home').checked,
		'Another location':s('#another-location').checked
	};
	var selectCity = s('#selectCity>option');
	var bio = s('#change_additional').value;
	var input_file = s('#photo');
	var base64 = s('#canvas').toDataURL('image/jpeg',0.7);

	var MinimumWage = s("#change_minimalprice").value;
	var daysArr = sa("#selectday>option");
	var PreferredDaysOfWeek = [];
	daysArr.forEach(function (element) {
		PreferredDaysOfWeek.push(element.value);
	});

	var selectSubject = sa('#selectSubject>option');
	var subjectsIds = [];
	selectSubject.forEach(function (element) {
		var name = element.value;
		subjectsIds.push(getSubjectId(element.value));
	});
	

	PreferredDaysOfWeek = PreferredDaysOfWeek.join(',');
	console.log(PreferredDaysOfWeek);

	if(!id||!name||!surname||!selectCity||!bio||!input_file||!base64){
		alert("Something Wrong");
		return;
	}
	var cityId = getCityId(selectCity.value);
	USR_DATA["CityId"] = cityId;
	USR_DATA["Picture"] = base64;
	USR_DATA["Name"] = name;
	USR_DATA["Surname"] = surname;
	USR_DATA["Address"] = JSON.stringify(checkers);
	USR_DATA["Description"] = bio;
	console.log(USR_DATA);
	$.ajax({
		type: "PUT",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(USR_DATA),
		url: URL_SERVER+"Odata/Users("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				updateTeacherInfo(USR_DATA,MinimumWage,PreferredDaysOfWeek,subjectsIds);
				break;
				default:
				alert("ServerError");
				break;
			}

		}
	});
	playloaderOff();
}
// for Profile Student js
function ProfileStudent(AUTHENTICATE){
	isProfileFilled();
	renderRight();
	renderLeft();
	setCities('#selectCity');
	uploadPhoto("#photo","#canvas");

	var modal = s("#modal2"),
	modalOverlay = s("#modal-overlay2"),
	closeButton = s("#close-button2");
	let openButton = s(".edit");
	closeButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});
	openButton.addEventListener("click", function () {
		toggle(modal);
		toggle(modalOverlay);
	});
	let daywork = $("#selectday").selectize({
		plugins: ['remove_button'],
		create: true,
		sortField: 'text'
	});

	var USR_DATA = getUser(AUTHENTICATE);
	s("#place").innerText = getCityName(USR_DATA["CityId"]);
	s("#additional_text").innerText = USR_DATA["Description"] 

	var location = s("#location");
	location.innerHTML = '';
	for(var prop in USR_DATA["Address"]){
		if(USR_DATA["Address"][prop]){
			var span = document.createElement('span');
			span.className = 'location text-nano';
			span.innerText = prop;
			location.appendChild(span);
		}
	}
	var photos = sa(".CurrentUser_Photo");
	[].forEach.call(photos, function(photo){
		photo.src = USR_DATA["Picture"];
	});
	var names = sa(".CurrentUser_Photo");
	[].forEach.call(names, function(name){
		name.innerText = USR_DATA["Name"]+" "+USR_DATA["Surname"];
	}); 
	var canvas = s("#canvas");
	var ctx = canvas.getContext("2d");

	var image = new Image();
	image.onload = function() {
		ctx.drawImage(image, 0, 0);
	};
	image.src =USR_DATA["Picture"];
	s("#change_name").value = USR_DATA["Name"];
	s("#change_surname").value = USR_DATA["Surname"];
	s("#change_additional").value = USR_DATA["Description"];
	s("#student-home").checked = USR_DATA["Address"]['Student`s home'];
	s("#tutor-home").checked = USR_DATA["Address"]['Tutors`s home'];
	s("#another-location").checked = USR_DATA["Address"]['Another location'];
	
    // var select = s("#selectCity");
    // var cityName = getCityName(USR_DATA["CityId"]);
    // var city = document.createElement("option");
    // city.setAttribute("value", cityName);
    // city.setAttribute("selected","selected");
    // select.appendChild(city);
    // var selectDiv = s(".Search>.selectize-control>.selectize-input");
    // var cityDiv =  document.createElement("div");
    // cityDiv.setAttribute("data-value", cityName);
    // cityDiv.classList.add( "item");
    // cityDiv.innerHTML = cityName + '<a href="javascript:void(0)" class="remove" tabindex="-1" title="Remove">×</a>';
    // selectDiv.appendChild(cityDiv);
}
//<option value="Aleksandrovsk" selected="selected">Aleksandrovsk</option>
// <div class="selectize-input items has-options has-items not-full">
//     <div data-value="Alupka" class="item">
//         Alupka
//         <a href="javascript:void(0)" class="remove" tabindex="-1" title="Remove">×</a>
//     </div>
//     <input type="text" autocomplete="off" tabindex="" style="width: 4px; opacity: 1; position: relative; left: 0px;">
// </div>
function ChangeProfileStudent(AUTHENTICATE){
	playloaderOn();
	var USR_DATA = getUser(AUTHENTICATE);
	var id = AUTHENTICATE;
	var name = s('#change_name').value;
	var surname = s('#change_surname').value;
	var checkers = {
		'Student`s home':s('#student-home').checked,
		'Tutors`s home':s('#tutor-home').checked,
		'Another location':s('#another-location').checked
	};
	var selectCity = s('#selectCity>option');
	var bio = s('#change_additional').value;
	var input_file = s('#photo');
	var base64 = s('#canvas').toDataURL('image/jpeg',0.7);
	if(!id||!name||!surname||!selectCity||!bio||!input_file||!base64){
		alert("Something Wrong");
		return;
	}
	var cityId = getCityId(selectCity.value);
	USR_DATA["CityId"] = cityId;
	USR_DATA["Picture"] = base64;
	USR_DATA["Name"] = name;
	USR_DATA["Surname"] = surname;
	USR_DATA["Address"] = JSON.stringify(checkers);
	USR_DATA["Description"] = bio;
	console.log(USR_DATA);
	$.ajax({
		type: "PUT",
		headers: {'Content-type':'application/json'},
		dataType: 'json',
		async: false,
		data: JSON.stringify(USR_DATA),
		url: URL_SERVER+"Odata/Users("+id+")",
		success: function (data, textStatus, xhr) {
			var status = xhr.status;
			switch(status){
				case 204:
				location.href = URL_PATH+PROFILE_PAGE_STUDENT;
				break;
				default:
				alert("ServerError");
				break;
			}

		}
	});
	playloaderOff();
}
// for Search Tutor js
function searchTutor(AUTHENTICATE){
	isProfileFilled();
	renderLeft();
	var lowerSlider = document.querySelector("#lower");
	var upperSlider = document.querySelector("#upper");

	document.querySelector("#two").value = upperSlider.value;
	document.querySelector("#one").value = lowerSlider.value;

	var lowerVal = parseInt(lowerSlider.value);
	var upperVal = parseInt(upperSlider.value);

	upperSlider.oninput = function () {
		lowerVal = parseInt(lowerSlider.value);
		upperVal = parseInt(upperSlider.value);

		if (upperVal < lowerVal + 4) {
			lowerSlider.value = upperVal - 4;
			if (lowerVal == lowerSlider.min) {
				upperSlider.value = 4;
			}
		}
		document.querySelector("#two").value = this.value;
		let one = document.querySelector("#one");
		let two = document.querySelector("#two");
	};

	lowerSlider.oninput = function () {
		lowerVal = parseInt(lowerSlider.value);
		upperVal = parseInt(upperSlider.value);
		if (lowerVal > upperVal - 4) {
			upperSlider.value = lowerVal + 4;
			if (upperVal == upperSlider.max) {
				lowerSlider.value = parseInt(upperSlider.max) - 4;
			}
		}
		document.querySelector("#one").value = this.value;
		let one = document.querySelector("#one");
		let two = document.querySelector("#two");
	};
	setCities('#selectcity');
	setSubjectsForm('#selectSubject');
	$("#filter-rate").selectize({
		create: true,
		sortField: "text"
	});

	function enableForm() {
		$("#sort_form :input").prop("disabled", false);
		$("#one").prop("disabled", true);
		$("#two").prop("disabled", true);
	}


	var id = AUTHENTICATE;
	var TeachersArray = getAllTeachers();
	console.log(TeachersArray);
	renderAllTeachers(TeachersArray,s(".profile-center"));
}
// for view Tutor js
function viewTutor(AUTHENTICATE){
	renderLeft();
	var params = parseURLParams(window.location.href);
	var CurrentUser = getUser(AUTHENTICATE);
	var teacher = getUser(params["id"]);

	let comment = s("#profile-comment");
	comment.addEventListener("click", function () {
		let first = s(".first-slide");
		let second = s(".second-slide");
		toggle(first);
		toggle(second);
	});


	setSubjectsProfile("#subjectsProfile",teacher);
	s("#place").innerText = getCityName(teacher["CityId"]);
	s("#additional_text").innerText = teacher["Description"] 
	s("#price").innerText = teacher["TeacherInfo"]["MinimumWage"];
	var location = s("#location");
	location.innerHTML = '';
	for(var prop in teacher["Address"]){
		if(teacher["Address"][prop]){
			var span = document.createElement('span');
			span.className = 'location text-nano';
			span.innerText = prop;
			location.appendChild(span);
		}
	}
	var dates = teacher["TeacherInfo"]["PreferredDaysOfWeek"].split(", ");
	dates.forEach(function (element) {
		$("[name='"+element+"']").addClass('active').removeClass('passive');
	});
	printRole(".CurrentUser_Role",CurrentUser);
	printRole(".Teacher_Role", teacher);
	var photos = sa(".CurrentUser_Photo");
	[].forEach.call(photos, function(photo){
		photo.src = CurrentUser["Picture"];
	});
	var names = sa(".CurrentUser_Name");
	[].forEach.call(names, function(name){
		name.innerText = CurrentUser["Name"]+" "+CurrentUser["Surname"];
	}); 
	photos = sa(".Teacher_photo");
	[].forEach.call(photos, function(photo){
		photo.src = teacher["Picture"];
	});
	names = sa(".Teacher_Name");
	[].forEach.call(names, function(name){
		name.innerText = teacher["Name"]+" "+teacher["Surname"];
	}); 
}



