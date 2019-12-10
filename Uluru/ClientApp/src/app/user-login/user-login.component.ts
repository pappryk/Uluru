import { Component, Inject } from '@angular/core';

@Component({
    selector: 'user-login',
    templateUrl: './user-login.component.html',
})
export class UserLogin {
    public email: string;
    public password: string;

    public async onClick() {
        let baseUrl = document.getElementsByTagName('base')[0].href;
        let url = baseUrl + "api/users/login";
        let data = JSON.stringify({
            email: this.email,
            password: this.password,
        });

        let response = await fetch(url, {
            method: 'post',
            headers: {
                'content-type': 'application/json'
            },
            body: data
        });

        console.log(localStorage.getItem('token'));
    }
}
