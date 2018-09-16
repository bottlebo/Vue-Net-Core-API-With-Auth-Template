import { Observable } from 'rxjs/Rx';

export class BaseService {

    api = 'http://localhost:5000/api';

    handleError(error) {
    const applicationError = error.headers['Application-Error'];

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

    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
    return Observable.throw(modelStateErrors || 'Server error');
  }
}