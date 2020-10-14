import { singleton } from 'tsyringe';
import { AlertViewModel } from '../Alert';
import { PageViewModel } from '../PageViewModel';
import * as template from './MainPage.html';

@singleton()
export class MainPageViewModel extends PageViewModel {
    constructor() {
        super(template);
    }

    readonly alert = new AlertViewModel();
}