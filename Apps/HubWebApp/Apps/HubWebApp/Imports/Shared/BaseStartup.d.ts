import { apiConstructor, AppApi } from "./AppApi";
import { PageFrame } from "./PageFrame";
export declare let defaultApi: apiConstructor<AppApi>;
export declare abstract class BaseStartup {
    build(): PageFrame;
    protected abstract getDefaultApi(): any;
}
