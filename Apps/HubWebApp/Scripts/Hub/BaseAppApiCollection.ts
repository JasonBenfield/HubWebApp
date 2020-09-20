import { AppApiEvents } from './AppApiEvents';
import { AppApi } from './AppApi';

export class BaseAppApiCollection {
    constructor(private readonly events: AppApiEvents) {
    }

    private _thisApp: AppApi;

    protected readonly apps: {
        [name: string]: AppApi
    } = {};

    protected addThisApp<T extends AppApi>(createApp: (evts: AppApiEvents) => T) {
        let app = this.addApp<T>(createApp);
        this._thisApp = app;
        return app;
    }

    protected addApp<T extends AppApi>(createApp: (evts: AppApiEvents) => T) {
        let app = createApp(this.events);
        this.apps[app.name] = app;
        return app;
    }

    get thisApp() { return this._thisApp; }
}

export let baseApi: BaseAppApiCollection;

export function setBaseApi(_api: BaseAppApiCollection) {
    baseApi = _api;
}
