var viewModel =  () => {
    var videoBlogs = ko.observableArray(window.videoBlogs),
        selectedVideoBlog = ko.observable(undefined),
        groupedVideoBlogs = ko.computed( ()=> {
            var rows = [], current = [];
            rows.push(current);
            for (var i = 0; i < window.videoBlogs.length; i += 1) {
                current.push(window.videoBlogs[i]);
                if (((i + 1) % window.numberOfItemsOnRow) === 0) {
                    current = [];
                    rows.push(current);
                }
            }
            return rows;
        }, this),
        handleVideoBlogSelection =  (data, e) => {
            if (!selectedVideoBlog() || selectedVideoBlog().id !== data.id) {
                selectedVideoBlog(data);
            }
        },
        closeVideoBlogModal = () => {
            selectedVideoBlog(undefined);
        };


    return {
        videoBlogs: videoBlogs,
        groupedVideoBlogs: groupedVideoBlogs,
        selectedVideoBlog: selectedVideoBlog,
        handleVideoBlogSelection: handleVideoBlogSelection,
        closeVideoBlogModal: closeVideoBlogModal
    }
};

// Bind the ViewModel to the View using Knockout
ko.applyBindings(viewModel, document.getElementById("video-gallery"));