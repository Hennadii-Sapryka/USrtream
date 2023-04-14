 var viewModelProf = function() {
     isVisible: ko.observable(false),


        closeEditPopUp= () => {
            modal_container_edit.classList.remove('show');
        },
        openEditPopUp= () => {
            modal_container_edit.classList.add('show');
        },
        openPasswordPopUp= () => {
            modal_container.classList.add('show');
        },
        closePasswordPopUp= () => {
            modal_container.classList.remove('show');
        }
    }

ko.applyBindings(new viewModelProf());


