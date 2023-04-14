const social = document.querySelector('.nav-menu');
const mobile = document.body.offsetWidth;

const copyright = document.querySelector('.site-copyright');
const socialContainer = document.querySelector('.social-media-container');
const clonSocialLink = socialContainer.cloneNode(true);
clonSocialLink.append(copyright);

if (matchMedia) {
	var screen = window.matchMedia("(max-width:767px)");
	screen.addListener(changes);
	changes(screen);
}

function changes(screen) {
	(screen.matches)
		? social.append(clonSocialLink)
		: clonSocialLink.remove()
}


