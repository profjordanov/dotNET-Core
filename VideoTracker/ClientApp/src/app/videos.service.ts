import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import { Video } from "./video";

@Injectable()
export class VideosService {

  constructor(private httpClient: HttpClient) { }

  // Getting the list of videos
  getVideos(): Observable<Video[]> {
    return this.httpClient.get<Video[]>('/api/Videos');
  }

  // Adding a new video by calling post
  addVideo(video: Video) {
    return this.httpClient.post('/api/Videos', video);
  }

  // Marking a video as watched using the PUT API
  setWatched(video: Video) {
    video.watched = true;

    return this.httpClient.put(`/api/Videos/${video.id}`, video);
  }
