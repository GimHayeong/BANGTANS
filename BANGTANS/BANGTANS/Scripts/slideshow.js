var slideIndex = 1;
showSlideItem();

// Next/previous controls
function nextSlideItem(n) {
    slideIndex += n;
    showSlideItem();
}

// Thumbnail image controls
function currentSlideItem(n) {
    slideIndex = n;
    showSlideItem();
}

function showSlideItem() {
    var i;

    var slides = $(".slideItem");
    var dots = $(".dot");

    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }

    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";

    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1; }
    if (slideIndex < 1) { slideIndex = slides.length; }

    setTimeout(showSlideItem, 3000);
}