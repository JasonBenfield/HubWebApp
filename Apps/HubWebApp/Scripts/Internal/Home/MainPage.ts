import 'reflect-metadata';
import { MainPageViewModel } from "./MainPageViewModel";
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';

@singleton()
class MainPage {
    constructor(private readonly viewModel: MainPageViewModel) {
        this.viewModel.telephoneNumber('');
    }
}
startup(MainPageViewModel, MainPage);