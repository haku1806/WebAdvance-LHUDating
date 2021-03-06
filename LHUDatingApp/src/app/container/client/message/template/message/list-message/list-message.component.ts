import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Group } from 'src/app/core/model/group';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { ChatBoardService } from 'src/app/core/service/chat-board.service';

@Component({
  selector: 'app-list-message',
  templateUrl: './list-message.component.html',
  styleUrls: ['./list-message.component.scss']
})
export class ListMessageComponent implements OnInit {

  @Output() onClick = new EventEmitter<Group>();

  datas: Group[] = [];
  groupSelected!: string;

  constructor(
    private chatBoardService: ChatBoardService,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.chatBoardService.getHistory()
      .subscribe((resp: any) => {
        this.datas = JSON.parse(resp["data"]);
      }, (error) => {
        console.log(error)
      })
  }

  openMessage(groupCode: any) {
    this.groupSelected = groupCode;
    this.onClick.emit(this.datas.find(x => x.Code == groupCode));
  }

}
