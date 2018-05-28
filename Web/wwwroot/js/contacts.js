var Contact = (function () {
    function Contact(data) {
        _.extend(this, data);
    }

    return Contact;
})();

var ContactViewModel = (function () {
    function ContactViewModel() {
        this.Name = ko.observable();
        this.Email = ko.observable();
        this.Contacts = ko.observableArray([]);
    }

    _.extend(ContactViewModel.prototype, {
        Load: function () {
            var $this = this;
            $.getJSON('api/Contact/List')
                .done(function (data) {
                    var contacts = _(data).map(function (contact) {
                        return new Contact(contact);
                    });
                    $this.Contacts(contacts);
                })
        },

        Create: function () {
            var contact = {
                Name: this.Name(),
                Email: this.Email()
            };

            var $this = this;
            $.ajax({
                type: 'POST',
                url: 'api/Contact/Create',
                data: JSON.stringify(contact),
                success: function () {
                    $this.Contacts.push(new Contact(contact));
                },
                contentType: "application/json",
                dataType: 'json'
            });
        }
    });

    return ContactViewModel;
})();