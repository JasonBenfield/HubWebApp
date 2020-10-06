import { MainPageViewModel } from "./MainPageViewModel";
import { TelephoneNumber } from "../TelephoneNumber";
import { startup } from 'xtistart';

class MainPage {
    constructor(private readonly viewModel: MainPageViewModel) {
        this.viewModel.telephoneNumber(new TelephoneNumber(864, 555, 1234).toString());
    }
}
startup(
    () => new MainPageViewModel(),
    vm => new MainPage(<MainPageViewModel>vm)
);
