import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private readonly storageKey = 'portfolio-theme';

  initializeTheme(): void {
    const savedTheme = localStorage.getItem(this.storageKey);

    if (savedTheme === 'dark') {
      document.body.classList.add('dark-theme');
    }
  }

  toggleTheme(): void {
    const isDark = document.body.classList.contains('dark-theme');

    if (isDark) {
      document.body.classList.remove('dark-theme');
      localStorage.setItem(this.storageKey, 'light');
    } else {
      document.body.classList.add('dark-theme');
      localStorage.setItem(this.storageKey, 'dark');
    }
  }

  isDarkMode(): boolean {
    return document.body.classList.contains('dark-theme');
  }
}
