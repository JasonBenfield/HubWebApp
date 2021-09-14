import { apiConstructor, AppApi } from "./AppApi";
import { ModalErrorComponent } from "./Error/ModalErrorComponent";
export declare class AppApiFactory {
    private _defaultApiType;
    set defaultApiType(defaultApi: apiConstructor<AppApi>);
    defaultApi(modalError: ModalErrorComponent): AppApi;
    api<TApi extends AppApi>(apiCtor: apiConstructor<TApi>, modalError: ModalErrorComponent): TApi;
}
