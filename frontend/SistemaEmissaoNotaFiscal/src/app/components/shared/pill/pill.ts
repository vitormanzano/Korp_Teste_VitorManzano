import { Component, input } from '@angular/core';

export type PillVariant = 'open' | 'closed' | 'danger';

@Component({
  selector: 'app-pill',
  imports: [],
  templateUrl: './pill.html',
  styleUrl: './pill.css',
})
export class Pill {
  variant = input.required<PillVariant>();
  label = input.required<string>();
}
