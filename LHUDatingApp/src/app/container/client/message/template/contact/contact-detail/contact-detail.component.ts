import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
declare const $: any;

@Component({
  selector: 'app-contact-detail',
  templateUrl: './contact-detail.component.html',
  styleUrls: ['./contact-detail.component.scss']
})
export class ContactDetailComponent implements OnInit {

  @Input() contact!: any;

  toggleTabChat: boolean = false;
  constructor(
  ) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ("contact" in changes) {
      this.toggleTabChat = false;
    }
  }

  chat() {
    this.toggleTabChat = true;
  }
}
