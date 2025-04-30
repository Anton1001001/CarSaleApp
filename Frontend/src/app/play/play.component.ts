import { Component, OnInit } from '@angular/core';
import { PlayService } from './data-access/play.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-play',
  imports: [CommonModule],
  templateUrl: './play.component.html',
  styleUrl: './play.component.css'
})
export class PlayComponent implements OnInit {

  message: string | undefined;

  constructor(private playService: PlayService) {}

  ngOnInit(): void {
    this.playService.getPlayers().subscribe({
      next: (response: any) => { 
        this.message = response.message;
        console.log(this.message)
      },
      error: (error) => {console.log(error)}
    });
  }

}
