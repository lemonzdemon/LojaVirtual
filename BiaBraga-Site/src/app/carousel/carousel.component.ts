import { Component, OnInit } from '@angular/core';
import { UrlAPI } from '../_utils/UrlAPI';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  myInterval = 4000;
  activeSlideIndex = 0;

  slides = [
    {image: UrlAPI.UrlImagesCarousel + '1.jpg'},
    {image: UrlAPI.UrlImagesCarousel + '2.jpg'}
  ];

  constructor() { }

  ngOnInit() {
  }

}
