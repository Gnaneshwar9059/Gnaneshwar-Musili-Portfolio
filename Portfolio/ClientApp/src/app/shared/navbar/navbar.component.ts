import {
Component,
HostListener,
OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{

constructor(private router: Router){}

  menuOpen = false;

  activeSection = 'hero';

  sections = [
    'hero',
    'about',
    'skills',
    'experience',
    'projects',
    'contact'
  ];

  isDark = false;

ngOnInit() {

  const theme = localStorage.getItem('theme');

  if (theme === 'dark') {

    this.isDark = true;

    document.body.classList.add('dark-theme');

  }

}

toggleTheme() {

  this.isDark = !this.isDark;

  document.body.classList.toggle('dark-theme');

  localStorage.setItem(
    'theme',
    this.isDark ? 'dark' : 'light'
  );

}

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  closeMenu() {
    this.menuOpen = false;
  }

  @HostListener('window:scroll')

onScroll() {

  for (const section of this.sections) {

    const element = document.getElementById(section);

    if (!element) continue;

    const top = element.offsetTop - 120;

    const bottom = top + element.offsetHeight;

    if (window.scrollY >= top && window.scrollY < bottom) {

      this.activeSection = section;

      if(section==="hero"){

        history.replaceState(null,""," ");

      }

    }

  }

}

}
