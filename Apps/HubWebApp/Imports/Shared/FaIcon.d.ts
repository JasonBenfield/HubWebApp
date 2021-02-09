import { ContextualClass } from './ContextualClass';
import { HtmlComponent } from './Html/HtmlComponent';
import { HtmlComponentViewModel } from './Html/HtmlComponentViewModel';
export declare class FaIconViewModel extends HtmlComponentViewModel {
    constructor();
}
export declare class FaIconNames {
    static readonly login = "sign-in-alt";
    static readonly save = "check";
    static readonly cancel = "times";
}
export declare type FaIconAnimation = 'spin' | 'pulse';
export declare class FaIcon extends HtmlComponent {
    constructor(name?: string, vm?: FaIconViewModel);
    private name;
    protected readonly vm: FaIconViewModel;
    private color;
    setColor(color?: ContextualClass): void;
    private prefix;
    regularStyle(): void;
    solidStyle(): void;
    private setPrefix;
    setName(name: string): void;
    private normalizeName;
    makeFixedWidth(): void;
    private size;
    resize(size: '' | 'xs' | 'sm' | 'lg' | '1X' | '2X' | '3X' | '4X' | '5X' | '6X' | '7X' | '8X' | '9X' | '10X'): void;
    makeBordered(): void;
    private rotation;
    rotate(howMuch: 0 | 90 | 180 | 270): void;
    private pulled;
    pullLeft(): void;
    pullRight(): void;
    private pull;
    private animation;
    startAnimation(animation: FaIconAnimation): void;
    stopAnimation(): void;
}
