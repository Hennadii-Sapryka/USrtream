document.querySelector('span.search-box-item.advanced-filter-toggle').addEventListener('click', shadowBody);
document.querySelector('span.search-box-item.advanced-filter-toggle').addEventListener('click', getTransparentTable);
document.querySelector('.filter-cross-icon').addEventListener('click', shadowBody);
document.querySelector('.filter-cross-icon').addEventListener('click', getTransparentTable);

function shadowBody(){
	document.body.classList.toggle('shadowness');
};

function getTransparentTable() {
	var table = document.querySelector('table');
	var tablePages = document.querySelector('.table-pages.flex.space-between');
	var pageButtonCorrent = document.querySelector('.page-button.current-page');
	if (table!=null) {
		table.classList.toggle('shadow');
		tablePages.classList.toggle('translucent');
		pageButtonCorrent.classList.toggle('translucent');
	}
};
