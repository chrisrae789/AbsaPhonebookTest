import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { HeadingService } from '../heading.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

   navigateBack() {

    this.location.back();
  }

  headingtitle : String;

  constructor(private location: Location, private heading : HeadingService) {
  }

  ngOnInit() {
    this.heading.getTitle().subscribe(result =>
      {
         this.headingtitle = result;
      });
  }

}
