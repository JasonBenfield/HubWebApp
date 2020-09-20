import { UserPageViewModel } from "./UserPageViewModel";
import { Alert } from '../Alert';
import { UrlBuilder } from '../UrlBuilder';
import { startup } from 'cpwstart';
import { WebPage } from '../WebPage';
import { baseApi } from '../BaseAppApiCollection';

class UserPage {
    constructor(private readonly vm: UserPageViewModel) {
        this.goToReturnUrl();
    }

    private goToReturnUrl() {
        this.alert.info('Opening Page...');
        let urlBuilder = UrlBuilder.current();
        let returnUrl = urlBuilder.getQueryValue('returnUrl');
        if (returnUrl) {
            returnUrl = decodeURIComponent(returnUrl);
        }
        returnUrl = baseApi.thisApp.url.addPart(returnUrl).getUrl();
        new WebPage(returnUrl).open();
    }

    private readonly alert = new Alert(this.vm.alert);
}
startup(
    () => new UserPageViewModel(),
    vm => new UserPage(vm)
);