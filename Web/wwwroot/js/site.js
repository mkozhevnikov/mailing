(function ($, _, ko) {
    var view = $('.contact')[0];
    var viewModel = new ContactViewModel();
    ko.applyBindings(viewModel, view);
    viewModel.Load();
})(jQuery, _, ko);