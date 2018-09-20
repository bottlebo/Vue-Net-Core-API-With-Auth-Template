import { Observable } from 'rxjs/Rx';
import store from '../store'
import router from '../router'
export class BaseService {

    api = 'http://localhost:5000/api';

    handleError(error) {
    const applicationError = error.headers['Application-Error'];
console.log(error)
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
    if(error.status == 401)
      store.dispatch('auth/authLogout', null, { root: true }).then(()=>{router.push("/login");});
    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
    return Observable.throw(modelStateErrors || 'Server error');
  }
}