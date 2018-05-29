(function ($, _, ko) {
    var viewModel = new ContactViewModel();
    viewModel.Load();

    class ContactContainer extends React.Component {
        render() {
            return (
                <div id="contact-container">
                    <h3>Add new</h3>
                    <form>
                        <label htmlFor="name">
                            Name:
                            <input type="text" id="name" data-bind="value: Name"/>
                        </label>
                        <label htmlFor="email">
                            Email:
                            <input type="text" id="email" data-bind="value: Email"/>
                        </label>
                        <button type="button" data-bind="click: Create">Create</button>
                    </form>
                    
                    <div id="contact-list">
                        <h2 data-bind="visible: Contacts.length">Contacts:</h2>
                        <ul data-bind="foreach: Contacts">
                            <li data-bind="id: Id">
                                <p data-bind="text: Name + ' - ' + Email"></p>
                                <a data-bind="click: console.log"></a>
                            </li>
                        </ul>
                    </div>
                </div>
            );
        }
    }

    var appRoot = document.getElementById("app");
    ReactDOM.render(<ContactContainer model={viewModel} />, appRoot);

    var view = $('#contact-container')[0];
    ko.applyBindings(viewModel, view);
})(jQuery, _, ko);