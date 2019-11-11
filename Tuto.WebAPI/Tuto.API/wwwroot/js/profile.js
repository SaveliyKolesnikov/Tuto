function toggle(el) {
    el.style.display = (el.style.display == 'none') ? 'block' : 'none'
}


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