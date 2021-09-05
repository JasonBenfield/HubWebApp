import { BlockViewModel } from './Html/BlockViewModel';
import { HtmlComponent } from './Html/HtmlComponent';
export declare class MessageAlert extends HtmlComponent {
    constructor(vm?: BlockViewModel);
    protected readonly vm: BlockViewModel;
    private readonly alert;
    private readonly textBlock;
    private _message;
    get message(): string;
    get hasMessage(): boolean;
    private readonly _messageChanged;
    readonly messageChanged: import("./Events").DefaultEventHandler<string>;
    clear(): void;
    success(message: string): void;
    info(message: string): void;
    infoAction(message: string, a: () => Promise<any>): Promise<void>;
    warning(message: string): void;
    danger(message: string): void;
    private setMessage;
    private debouncedSetMessage;
    private updateVmMessage;
}
