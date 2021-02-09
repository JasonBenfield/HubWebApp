import { Small } from './Small';
import { SmallViewModel } from './SmallViewModel';
import { TextSpan } from './TextSpan';
export declare class TextSmall extends Small {
    constructor(text?: string, vm?: SmallViewModel);
    protected readonly vm: SmallViewModel;
    readonly textSpan: TextSpan;
    setText(text: string): void;
}
