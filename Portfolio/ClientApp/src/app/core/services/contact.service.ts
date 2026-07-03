import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { ContactRequest } from '../models/contact-request';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private readonly http = inject(HttpClient);

  private readonly api = '/api/contact';

  send(request: ContactRequest): Observable<any> {
    return this.http.post(this.api, request);
  }
}
