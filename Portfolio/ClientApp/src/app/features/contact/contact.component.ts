import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';

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

  sendMessage(form: NgForm) {

    // Guard: don't send if the form is invalid.
    // (Button is disabled in this case too, but this protects
    // against submission via Enter key on an input.)
    if (form.invalid) {
      return;
    }

    this.sending = true;

    this.successMessage = '';

    this.errorMessage = '';

    this.contactService.send(this.model).subscribe({

      next: (response) => {

        this.successMessage = response.message;

        this.sending = false;

        const emptyModel: ContactRequest = {
          name: '',
          email: '',
          phone: '',
          role: '',
          subject: '',
          message: ''
        };

        this.model = emptyModel;

        // Resets values, touched/dirty state, and submitted state
        // together, so the form visually returns to its initial state.
        form.resetForm(emptyModel);
      },

      error: () => {

        this.errorMessage =
          'Failed to send message.';

        this.sending = false;
      }

    });

  }

}
