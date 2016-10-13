﻿/// <autosync enabled="true" />
/// <reference path="../areas/administration/scripts/custom/datetime-handlers.js" />
/// <reference path="../areas/administration/scripts/custom/error-handler.js" />
/// <reference path="../areas/administration/scripts/custom/grid-details-helpers.js" />
/// <reference path="../areas/administration/scripts/custom/product-images-upload.js" />
/// <reference path="../areas/administration/scripts/custom/product-main-image-dropdown.js" />
/// <reference path="../areas/public/scripts/custom/bootstrap-modal-helpers.js" />
/// <reference path="../areas/public/scripts/custom/igniteui-rating-handler.js" />
/// <reference path="../areas/public/scripts/igniteui/infragistics.core.unicode.js" />
/// <reference path="../areas/public/scripts/igniteui/infragistics.dv.js" />
/// <reference path="../areas/public/scripts/igniteui/infragistics.lob.js" />
/// <reference path="../areas/public/scripts/igniteui/infragistics.ui.rating.js" />
/// <reference path="../areas/public/scripts/igniteui/infragistics.util.unicode.js" />
/// <reference path="../areas/public/scripts/igniteui/jquery-ui.js" />
/// <reference path="../areas/public/scripts/igniteui/originals/infragistics.core.js" />
/// <reference path="bootstrap.min.js" />
/// <reference path="jquery.min.js" />
/// <reference path="jquery.signalr-2.2.0.min.js" />
/// <reference path="jquery.unobtrusive-ajax.min.js" />
/// <reference path="jquery.validate.min.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="jquery-2.2.1.js" />
/// <reference path="jquery-2.2.1.min.js" />
/// <reference path="kendo/cultures/kendo.culture.af.min.js" />
/// <reference path="kendo/cultures/kendo.culture.af-za.min.js" />
/// <reference path="kendo/cultures/kendo.culture.am.min.js" />
/// <reference path="kendo/cultures/kendo.culture.am-et.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-ae.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-bh.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-dz.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-eg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-iq.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-jo.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-kw.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-lb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-ly.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-ma.min.js" />
/// <reference path="kendo/cultures/kendo.culture.arn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.arn-cl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-om.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-qa.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-sa.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-sy.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-tn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ar-ye.min.js" />
/// <reference path="kendo/cultures/kendo.culture.as.min.js" />
/// <reference path="kendo/cultures/kendo.culture.as-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.az.min.js" />
/// <reference path="kendo/cultures/kendo.culture.az-cyrl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.az-cyrl-az.min.js" />
/// <reference path="kendo/cultures/kendo.culture.az-latn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.az-latn-az.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ba.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ba-ru.min.js" />
/// <reference path="kendo/cultures/kendo.culture.be.min.js" />
/// <reference path="kendo/cultures/kendo.culture.be-by.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bg-bg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bn-bd.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bn-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bo.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bo-cn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.br.min.js" />
/// <reference path="kendo/cultures/kendo.culture.br-fr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bs.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bs-cyrl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bs-cyrl-ba.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bs-latn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.bs-latn-ba.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ca.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ca-es.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ca-es-valencia.min.js" />
/// <reference path="kendo/cultures/kendo.culture.chr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.chr-cher.min.js" />
/// <reference path="kendo/cultures/kendo.culture.chr-cher-us.min.js" />
/// <reference path="kendo/cultures/kendo.culture.co.min.js" />
/// <reference path="kendo/cultures/kendo.culture.co-fr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.cs.min.js" />
/// <reference path="kendo/cultures/kendo.culture.cs-cz.min.js" />
/// <reference path="kendo/cultures/kendo.culture.cy.min.js" />
/// <reference path="kendo/cultures/kendo.culture.cy-gb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.da.min.js" />
/// <reference path="kendo/cultures/kendo.culture.da-dk.min.js" />
/// <reference path="kendo/cultures/kendo.culture.de.min.js" />
/// <reference path="kendo/cultures/kendo.culture.de-at.min.js" />
/// <reference path="kendo/cultures/kendo.culture.de-ch.min.js" />
/// <reference path="kendo/cultures/kendo.culture.de-de.min.js" />
/// <reference path="kendo/cultures/kendo.culture.de-li.min.js" />
/// <reference path="kendo/cultures/kendo.culture.de-lu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.dsb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.dsb-de.min.js" />
/// <reference path="kendo/cultures/kendo.culture.dv.min.js" />
/// <reference path="kendo/cultures/kendo.culture.dv-mv.min.js" />
/// <reference path="kendo/cultures/kendo.culture.el.min.js" />
/// <reference path="kendo/cultures/kendo.culture.el-gr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-029.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-au.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-bz.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-ca.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-gb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-hk.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-ie.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-jm.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-my.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-nz.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-ph.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-sg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-tt.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-us.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-za.min.js" />
/// <reference path="kendo/cultures/kendo.culture.en-zw.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-419.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-ar.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-bo.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-cl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-co.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-cr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-do.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-ec.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-es.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-gt.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-hn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-mx.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-ni.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-pa.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-pe.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-pr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-py.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-sv.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-us.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-uy.min.js" />
/// <reference path="kendo/cultures/kendo.culture.es-ve.min.js" />
/// <reference path="kendo/cultures/kendo.culture.et.min.js" />
/// <reference path="kendo/cultures/kendo.culture.et-ee.min.js" />
/// <reference path="kendo/cultures/kendo.culture.eu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.eu-es.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fa.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fa-ir.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ff.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ff-latn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ff-latn-sn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fi.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fi-fi.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fil.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fil-ph.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fo.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fo-fo.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-be.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-ca.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-cd.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-ch.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-ci.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-cm.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-fr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-ht.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-lu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-ma.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-mc.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-ml.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-re.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fr-sn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fy.min.js" />
/// <reference path="kendo/cultures/kendo.culture.fy-nl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ga.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ga-ie.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gd.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gd-gb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gl-es.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gn-py.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gsw.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gsw-fr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.gu-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ha.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ha-latn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ha-latn-ng.min.js" />
/// <reference path="kendo/cultures/kendo.culture.haw.min.js" />
/// <reference path="kendo/cultures/kendo.culture.haw-us.min.js" />
/// <reference path="kendo/cultures/kendo.culture.he.min.js" />
/// <reference path="kendo/cultures/kendo.culture.he-il.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hi.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hi-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hr-ba.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hr-hr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hsb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hsb-de.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hu-hu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hy.min.js" />
/// <reference path="kendo/cultures/kendo.culture.hy-am.min.js" />
/// <reference path="kendo/cultures/kendo.culture.id.min.js" />
/// <reference path="kendo/cultures/kendo.culture.id-id.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ig.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ig-ng.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ii.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ii-cn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.is.min.js" />
/// <reference path="kendo/cultures/kendo.culture.is-is.min.js" />
/// <reference path="kendo/cultures/kendo.culture.it.min.js" />
/// <reference path="kendo/cultures/kendo.culture.it-ch.min.js" />
/// <reference path="kendo/cultures/kendo.culture.it-it.min.js" />
/// <reference path="kendo/cultures/kendo.culture.iu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.iu-cans.min.js" />
/// <reference path="kendo/cultures/kendo.culture.iu-cans-ca.min.js" />
/// <reference path="kendo/cultures/kendo.culture.iu-latn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.iu-latn-ca.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ja.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ja-jp.min.js" />
/// <reference path="kendo/cultures/kendo.culture.jv.min.js" />
/// <reference path="kendo/cultures/kendo.culture.jv-latn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.jv-latn-id.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ka.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ka-ge.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kk.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kk-kz.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kl-gl.min.js" />
/// <reference path="kendo/cultures/kendo.culture.km.min.js" />
/// <reference path="kendo/cultures/kendo.culture.km-kh.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kn.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kn-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ko.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kok.min.js" />
/// <reference path="kendo/cultures/kendo.culture.kok-in.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ko-kr.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ku.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ku-arab.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ku-arab-iq.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ky.min.js" />
/// <reference path="kendo/cultures/kendo.culture.ky-kg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lb.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lb-lu.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lo.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lo-la.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lt.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lt-lt.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lv.min.js" />
/// <reference path="kendo/cultures/kendo.culture.lv-lv.min.js" />
/// <reference path="kendo/cultures/kendo.culture.mg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.mg-mg.min.js" />
/// <reference path="kendo/cultures/kendo.culture.mi.min.js" />
/// <reference path="kendo/cultures/kendo.culture.mi-nz.min.js" />
