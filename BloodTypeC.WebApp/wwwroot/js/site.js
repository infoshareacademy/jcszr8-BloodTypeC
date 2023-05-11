window.onload = function () {
    
    var targets = document.getElementsByClassName("tooltip-button");
    var tooltips = document.getElementsByClassName("tooltip-text");


    for (let i = 0; i < targets.length; i++) {
        targets[i].addEventListener('mouseover', () => {
            tooltips[i].style.display = 'block';
        }, false);
        targets[i].addEventListener('mouseleave', () => {
            tooltips[i].style.display = 'none';
        }, false);
    }
}