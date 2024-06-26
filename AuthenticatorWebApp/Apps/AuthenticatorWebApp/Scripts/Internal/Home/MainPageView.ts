﻿import { CssLengthUnit } from '@jasonbenfield/sharedwebapp/CssLengthUnit';
import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { BlockView } from '@jasonbenfield/sharedwebapp/Views/BlockView';
import { GridView } from '@jasonbenfield/sharedwebapp/Views/Grid';
import { TextHeading1View } from '@jasonbenfield/sharedwebapp/Views/TextHeadings';
import { AuthenticatorPageView } from '../AuthenticatorPageView';
import { LoginComponentView } from './LoginComponentView';

export class MainPageView extends AuthenticatorPageView {
    readonly loginComponent: LoginComponentView;

    constructor() {
        super();
        const grid = this.addView(GridView);
        grid.styleAsLayout();
        grid.height100();
        grid.setTemplateRows(CssLengthUnit.auto(), CssLengthUnit.flex(1));
        grid.addCell()
            .configure(c => c.addCssName('container'))
            .addView(TextHeading1View)
            .configure(th => th.setText('Login'));
        this.loginComponent = grid.addCell()
            .configure(c => c.positionRelative())
            .addView(BlockView)
            .configure(b => {
                b.positionAbsoluteFill();
                b.scrollable();
            })
            .addView(BlockView)
            .configure(b => {
                b.addCssName('container');
                b.setPadding(PaddingCss.top(3));
            })
            .addView(LoginComponentView);
    }
}