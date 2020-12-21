import 'reflect-metadata';
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';
import { MainPageViewModel } from './MainPageViewModel';

@singleton()
class MainPage {
    constructor(private readonly vm: MainPageViewModel) {
    }

}
startup(MainPageViewModel, MainPage);