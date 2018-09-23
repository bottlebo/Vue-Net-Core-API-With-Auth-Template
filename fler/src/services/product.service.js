//import axios from 'axios';
import { BaseService }   from './base.service';
//import { Observable } from 'rxjs/Rx';
//import { resolve } from 'path';

class ProductService extends BaseService {

    static instance; //: AdminService;

    constructor() {
    //console.log(super())    
        super();
     }

    static get Instance() {
       // Do you need arguments? Make it a regular method instead.
       return this.instance || (this.instance = new this());
    }

    all() {
        return this.get(`${this.api}/products/get`)
        /* return new Promise((resolve,reject) => {
            axios.get(url, {
                // data: category ,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
    
                resolve(response.data);
            })
                .catch(function (error) {
                    reject(error);
                });
        }) */
       /*  return Observable.fromPromise(axios.get(`${this.api}/categories/all`))
        .map((res) => res.data)
        .catch((error) => this.handleError(error.response)); */ 
    }
}

// export a singleton instance in the global namespace
export const productService = ProductService.Instance;