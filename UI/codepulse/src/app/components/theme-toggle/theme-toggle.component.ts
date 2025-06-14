import { Component } from '@angular/core';
import { ThemeService } from '../../services/theme.service';

@Component({
  selector: 'app-theme-toggle',
  template: `
    <button (click)="toggleTheme()" class="theme-toggle-btn">
      <i class="material-icons">{{ (isDark$ | async) ? 'light_mode' : 'dark_mode' }}</i>
    </button>
  `,
  styles: [`
    .theme-toggle-btn {
      background: none;
      border: none;
      cursor: pointer;
      padding: 8px;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;
      color: var(--text-color);
      transition: background-color 0.3s;
    }

    .theme-toggle-btn:hover {
      background-color: var(--hover-color);
    }

    .material-icons {
      font-size: 24px;
    }
  `]
})
export class ThemeToggleComponent {
  isDark$ = this.themeService.isDarkMode$;

  constructor(private themeService: ThemeService) {}

  toggleTheme() {
    this.themeService.toggleTheme();
  }
} 