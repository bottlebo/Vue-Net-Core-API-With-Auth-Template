import axios from 'axios';
//import { Credentials } from '@/models/credentials.interface';
import  { BaseService } from './base.service';
import { Observable } from 'rxjs/Rx';

class AuthService extends BaseService {

    static instance;//: AuthService;

    constructor() {  super(); }

    static get Instance() {
       // Do you need arguments? Make it a regular method instead.
       return this.instance || (this.instance = new this());
    }

    login(credentials){
        return Observable.fromPromise(axios.post(`${this.api}/auth/login`, credentials))
        .map((res) => res.data.auth_token)
        .catch((error) => this.handleError(error.response));
    }

    facebookLogin(accessToken) {
        return Observable.fromPromise(axios.post(`${this.api}/externalauth/facebook`, {accessToken}))
        .map((res) => res.data.auth_token)
        .catch((error) => this.handleError(error.response));
    }
}

// export a singleton instance in the global namespace
export const authService = AuthService.Instance;
