import * as ko from 'knockout';
var img = require("../../Images/asp-angular.jpg");

class AboutPageViewModel {
    image = img;
}

export default {
    viewModel: AboutPageViewModel,
    template: require('./about.html')
};