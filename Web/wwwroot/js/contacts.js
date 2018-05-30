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
            $.getJSON('api/Contact')
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
                url: 'api/Contact',
                data: JSON.stringify(contact),
                success: function (data) {
                    $this.Contacts.push(new Contact(data));
                },
                contentType: "application/json",
                dataType: 'json'
            });
        },

        Delete: function (contact) {
            var $this = this;
            $.ajax({
                type: 'DELETE',
                url: 'api/Contact?Id=' + contact.Id,
                success: function () {
                    var idx = $this.Contacts().indexOf(contact);
                    $this.Contacts.splice(idx, 1);
                },
                contentType: "application/json",
                dataType: 'json'
            })
        },
        
        Edit: function (contact) {
            var $this = this;
            var data = {
                Id: contact.Id, 
                Name: this.Name(),
                Email: this.Email()
            };
            $.ajax({
                type: 'PUT',
                url: 'api/Contact?Id=' + contact.Id,
                data: JSON.stringify(data),
                success: function () {
                    var idx = $this.Contacts().indexOf(contact);
                    $this.Contacts.splice(idx, 1);
                    $this.Contacts.splice(0, 0, new Contact(data))
                },
                contentType: "application/json",
                dataType: 'json'
            }); 
        }
    });

    return ContactViewModel;
})();