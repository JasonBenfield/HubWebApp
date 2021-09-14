import { Heading1 } from './Heading1';
import { Heading1ViewModel } from './Heading1ViewModel';
import { TextSpan } from './TextSpan';
export declare class TextHeading1 extends Heading1 {
    constructor(text?: string, vm?: Heading1ViewModel);
    protected readonly vm: Heading1ViewModel;
    readonly textSpan: TextSpan;
    setText(text: string): void;
}
