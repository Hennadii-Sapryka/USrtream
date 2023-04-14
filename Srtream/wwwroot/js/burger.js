document.querySelector('.head__burger').addEventListener('click', function(){
	document.querySelector('.nav-menu').classList.toggle('active');
	document.querySelector('.head__burger').classList.toggle('active');
	document.querySelector('.logo-container').classList.toggle('active');
	document.body.classList.toggle('lock');
  });