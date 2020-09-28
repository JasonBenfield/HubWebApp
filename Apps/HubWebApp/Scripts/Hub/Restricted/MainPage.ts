import { MainPageViewModel } from "./MainPageViewModel";
import { startup } from 'xtistart';

class MainPage {
    constructor(private readonly viewModel: MainPageViewModel) {
    }
}
startup(
    () => new MainPageViewModel(),
    vm => new MainPage(<MainPageViewModel>vm)
);