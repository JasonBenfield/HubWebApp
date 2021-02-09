import { TextSpanViewModel } from './TextSpanViewModel';
import { HtmlComponent } from "./HtmlComponent";
export declare class TextSpan extends HtmlComponent {
    constructor(text?: string, vm?: TextSpanViewModel);
    protected readonly vm: TextSpanViewModel;
    private text;
    setText(text: string): void;
    setTitleFromText(): void;
}
