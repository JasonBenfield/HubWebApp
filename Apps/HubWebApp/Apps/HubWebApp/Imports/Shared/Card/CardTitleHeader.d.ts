import { CardHeader } from './CardHeader';
import { BlockViewModel } from '../Html/BlockViewModel';
export declare class CardTitleHeader extends CardHeader {
    constructor(title?: string, vm?: BlockViewModel);
    private readonly textBlock;
    setText(text: string): void;
}
