var viewModel = new function ViewModel() {

	if (matchMedia) {
		var screen = window.matchMedia("(max-width:767px)");
		screen.addListener(changes);
		changes(screen);
	}

	function changes(screen) {
		(screen.matches)
			? this.imageUrl = ko.observable(window.normalSizeUrl)
			: this.imageUrl = ko.observable(window.mobileUrl);
	}
};
ko.applyBindings(viewModel);
