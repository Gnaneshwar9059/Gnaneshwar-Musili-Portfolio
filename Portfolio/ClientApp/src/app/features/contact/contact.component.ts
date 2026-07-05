import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ContactService } from '../../core/services/contact.service';
import { ContactRequest } from '../../core/models/contact-request';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
})
export class ContactComponent {

  private readonly contactService = inject(ContactService);

  model: ContactRequest = {
  name: '',
  email: '',
  phone: '',
  role: '',
  subject: '',
  message: ''
};

  sending = false;

  successMessage = '';

  errorMessage = '';

  sendMessage() {

    this.sending = true;

    this.successMessage = '';

    this.errorMessage = '';

    this.contactService.send(this.model).subscribe({

     next: (response) => {

        this.successMessage =  response.message;

//       //this.model = {
//   name: '',
//  email: '',
//  phone: '',
//  role: '',
//  subject: '',
//  message: ''
//};

        this.sending = false;
      },

      error: () => {

        this.errorMessage =
          'Failed to send message.';

        this.sending = false;
      }

    });

  }

}
