import { PageLoader } from '../Shared/PageLoader';
import { AppApiEvents } from '../Shared/AppApiEvents';
import { ConsoleLog } from '../Shared/ConsoleLog';
import { ModalErrorComponent } from '../Shared/Error/ModalErrorComponent';
import { container } from 'tsyringe';
import { HubAppApi } from '../Hub/Api/HubAppApi';
import { AuthenticatorAppApi } from '../Authenticator/Api/AuthenticatorAppApi';
import { AppApi } from '../Shared/AppApi';
import { HostEnvironment } from '../Shared/HostEnvironment';
import { LogoutUrl } from '../Authenticator/LogoutUrl';

export function startup(pageVM: any, page: any) {
    container.register('PageVM', { useFactory: c => c.resolve(pageVM) });
    container.register('Page', { useFactory: c => c.resolve(page) });
    container.register(
        AppApiEvents,
        {
            useFactory: c => new AppApiEvents((err) => {
                new ConsoleLog().error(err.toString());
                c.resolve(ModalErrorComponent).show(err.getErrors(), err.getCaption());
            })
        }
    );
    let hostEnvironment = new HostEnvironment();
    container.register(
        AuthenticatorAppApi,
        {
            useFactory: c => new AuthenticatorAppApi(
                c.resolve(AppApiEvents),
                pageContext.BaseUrl,
                hostEnvironment.isProduction ? '' : 'Current'
            )
        }
    );
    container.register(
        HubAppApi,
        {
            useFactory: c => new HubAppApi(
                c.resolve(AppApiEvents),
                `${location.protocol}//${location.host}`,
                'Current'
            )
        }
    )
    container.register(AppApi, { useFactory: c => c.resolve(HubAppApi) });
    container.register(
        'LogoutUrl',
        {
            useToken: LogoutUrl
        }
    );
    new PageLoader().load();
}