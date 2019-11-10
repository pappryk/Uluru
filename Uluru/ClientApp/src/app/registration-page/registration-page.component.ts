import { Component, Inject } from '@angular/core';

@Component({
  selector: 'registration-page',
  templateUrl: './registration-page.component.html'
})
export class RegistrationPageComponent {
    constructor() {}
    onClick(form) {
        let baseUrl = document.getElementsByTagName('base')[0].href;
        let myForm = document.getElementById('registerForm');
        let data = this.elementToJSON(myForm);
        console.log(data);

        fetch(baseUrl + 'api/users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: data
        }).then(response =>
            console.log(response.ok));
    }

    elementToJSON(element) {
        var obj = {};
        var elements = element.querySelectorAll("input");
        for (var i = 0; i < elements.length; ++i) {
            var element = elements[i];
            var name = element.name;
            var value = element.value;

            if (name) {
                obj[name] = value;
            }
        }

        return JSON.stringify(obj);
    }
}
