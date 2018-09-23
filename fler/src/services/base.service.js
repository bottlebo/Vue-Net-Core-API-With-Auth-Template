import { Observable } from 'rxjs/Rx';
import axios from 'axios';
//import store from '../store'
//import router from '../router'
export class BaseService {

  api = 'http://localhost:5000/api';

  handleError(error) {
    const applicationError = error.headers['Application-Error'];
    //console.log(error.data.statusCode)
    if (applicationError) {
      return Observable.throw(applicationError);
    }

    let modelStateErrors = '';

    if (error.data) {
      for (const key in error.data) {
        if (error.data[key]) {
          modelStateErrors += error.data[key] + '\n';
        }
      }
    }
    //if(error.status == 401 || error.status == 403)
    //store.dispatch('auth/authLogout', null, { root: true }).then(()=>{router.push("/login");});
    modelStateErrors = error.data.statusCode ? error.data.statusCode : 500;
    return Observable.throw(modelStateErrors);
  }
  get(url) {
    return new Promise(function (resolve, reject) {
      axios.get(url, {
        // data: category ,
        headers: { 'Content-Type': 'application/json' }
      }).then(function (response) {

        resolve(response.data);
      })
        .catch(function (error) {
          reject(error);
        });
    });
  }
}