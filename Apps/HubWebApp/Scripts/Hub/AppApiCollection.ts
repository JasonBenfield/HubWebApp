import { HubAppApi } from "./Api/HubAppApi";
import { AppApiEvents } from "./AppApiEvents";
import { baseApi, setBaseApi, BaseAppApiCollection } from './BaseAppApiCollection';

export class AppApiCollection extends BaseAppApiCollection {
    constructor(events: AppApiEvents) {
        super(events);
        this.Hub = this.addThisApp(evts=> new HubAppApi(evts, `${location.protocol}//${location.host}`));
    }

    readonly Hub: HubAppApi;
}

export function api() {
    return <AppApiCollection>baseApi;
}

export function hub() {
    return api().Hub;
}

export function setApi(_api: AppApiCollection) {
    setBaseApi(_api);
}