import axios from 'axios';
import { BaseService }   from './base.service';
import { Observable } from 'rxjs/Rx';

class AdminService extends BaseService {

    static instance; //: AdminService;

    constructor() {
    //console.log(super())    
        super();
     }

    static get Instance() {
       // Do you need arguments? Make it a regular method instead.
       return this.instance || (this.instance = new this());
    }

    profile() {
         
        return Observable.fromPromise(axios.get(`${this.api}/admin/profile`))
        .map((res) => res.data)
        .catch((error) => this.handleError(error.response)); 
    }
}

// export a singleton instance in the global namespace
export const adminService = AdminService.Instance;