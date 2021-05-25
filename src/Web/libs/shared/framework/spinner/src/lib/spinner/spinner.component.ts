import { Component, Input, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss'],
})
export class SpinnerComponent implements OnInit {
  @Input()
  loading: boolean;
  @Input()
  content: TemplateRef<any>;

  constructor() {}

  ngOnInit(): void {}
}
